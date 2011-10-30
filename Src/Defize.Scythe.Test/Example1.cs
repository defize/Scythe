namespace Defize.Scythe.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class Example1
    {
        [Test]
        public void Example()
        {
            // Define apply verb configuration mappings.
            var applyConfigurationMapper = new ArgumentMapper<ExampleApplyConfiguration>();
            applyConfigurationMapper.Map(x => x.Server)
                           .WithAliases("s", "svr")
                           .Default(@".\SQLEXPRESS")
                           .Validate(
                                y => y.Length(max: 15),
                                y => y.Matches("^[A-Za-z]*$"),
                                y => y.Custom(z => z.Length == 14)
                            );

            // bind commandline verbs to actions.
            var app = new ApplicationEntry<ExampleApplication>();
            app.Bind(applyConfigurationMapper)
               .To(x => x.Apply)
               .WithAliases("a", "aply");

            // Execute application
            app.Run(new[] { "apply", "Production" });
        }

        public class ExampleApplyConfiguration
        {
            public string Server { get; set; }
        }

        public class ExampleApplication
        {
            public void Apply(ExampleApplyConfiguration configuration)
            {
                // Apply the scripts.
            }
        }


    }
}
