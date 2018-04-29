using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ElementsLib.Elements.Config.Articles;
using ElementsLib.Elements;

namespace PayrollGeekApp
{
    using SourcePair = KeyValuePair<ElementsLib.Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<ElementsLib.Module.Interfaces.Elements.IArticleSource, string>>;
    using ResultPair = KeyValuePair<ElementsLib.Module.Interfaces.Elements.IArticleTarget, ResultMonad.Result<ElementsLib.Module.Interfaces.Elements.IArticleResult, string>>;

    using ElementsLib.Module.Libs;
    using ElementsLib.Module.Items;
    using ElementsLib.Module.Interfaces.Legalist;
    using ElementsLib.Matrixus;
    using ElementsLib.Legalist;
    using ElementsLib.Service.Permadom;
    using ElementsLib.Service.Matrixus;
    using ElementsLib.Service.Legalist;
    using ElementsLib.Service.Calculus;
    using ElementsLib.Module.Interfaces.Elements;

    static class ProgramModule
    {
        public static void CreatePayrollData(string configFolder)
        {
            var memoryService = new SimplePermadomService();

            var matrixService = new SimpleMatrixusService();

            matrixService.InitializeService(memoryService);

            var legalsService = new SimpleLegalistService();

            legalsService.InitializeService();

            IArticleSourceStore sourceStore = new ArticleSourceStore(matrixService.Profile());

            var sourceData = memoryService.GetArticleSourceData();

            sourceStore.LoadSourceData(sourceData);

            var calculService = new SimpleCalculusService(matrixService.Profile());

            calculService.InitializeService();

            Period evalPeriod = new Period(2018, 1);

            IPeriodProfile evalProfile = legalsService.GetPeriodProfile(evalPeriod);

            calculService.EvaluateStore(sourceStore, evalPeriod, evalProfile);

            List<SourcePair> evaluationPath = calculService.GetEvaluationPath();

            List<ResultPair> evaluationCase = calculService.GetEvaluationCase();

            string configFilePath = System.IO.Path.Combine(configFolder, "ARTICLES_PAYROLL.TXT");

            try
            {
                StreamWriter writerFile = new StreamWriter(configFilePath, false/* , Encoding.GetEncoding(1250) */);

                evaluationPath.ForEach((c) => writerFile.WriteLine(c.Description()));

                evaluationCase.ForEach((c) => writerFile.WriteLine(c.Description()));

                writerFile.Flush();

                writerFile.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }
    }
}
