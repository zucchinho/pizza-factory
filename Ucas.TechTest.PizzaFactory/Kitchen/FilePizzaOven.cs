namespace Ucas.TechTest.PizzaFactory.Kitchen
{
    using NLog;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Ucas.TechTest.PizzaFactory.Model;

    public class FilePizzaOven : IPizzaOven
    {
        private static readonly Lazy<string> OutputFilePathLazy = new Lazy<string>(
            () =>
            {
                return ConfigurationManager.AppSettings["FilePizzaOven.OutputFilePath"];
            });

        private readonly ILogger logger;
        private CancellationTokenSource internalTokenSource;

        public FilePizzaOven()
            : this(null)
        {
        }

        public FilePizzaOven(
            ILogger logger)
        {
            this.logger = logger ?? LogManager.CreateNullLogger();
            this.internalTokenSource = new CancellationTokenSource();

            // Delete any left-over file
            if (File.Exists(OutputFilePathLazy.Value))
            {
                this.logger.Warn(
                    "Output file already exists at: {0}",
                    OutputFilePathLazy.Value);
                File.Delete(OutputFilePathLazy.Value);
            }
        }

        public async Task CookAsync(
            IPizzaOrder pizzaOrder, 
            double cookingTimeMs, 
            CancellationToken cancellationToken)
        {
            var pizza = new Pizza(
                pizzaOrder,
                cookingTimeMs);

            this.logger.Debug(
                "Simulating cooking pizza {0} for {1}",
                pizza,
                cookingTimeMs);

            // Simulate cooking
            await Task.Delay(
                TimeSpan.FromMilliseconds(cookingTimeMs),
                cancellationToken);

            this.logger.Debug(
                "Finished cooking pizza. Writing to output file: {0}",
                OutputFilePathLazy.Value);

            try
            {
                using (var sw = new StreamWriter(OutputFilePathLazy.Value, true))
                {
                    await sw.WriteLineAsync(
                        pizza.ToString("C"));
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                this.logger.Fatal(
                    ex,
                    "The output file directory path does not exist: {0}",
                    OutputFilePathLazy.Value);
                throw;
            }
        }
    }
}
