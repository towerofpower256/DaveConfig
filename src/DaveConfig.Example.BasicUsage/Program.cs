using System;
using System.Collections.Generic;
using System.Text;

namespace DaveConfig.Example.BasicUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup the configuration
            MyAppContext.Config = new FileConfigurationProvider("my_config.xml");
            MyAppContext.Config.LoadConfiguration();

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");

            Console.WriteLine("Setting 'option1' to 'value1'");
            MyAppContext.Config.SetOption("option1", "value1");

            Console.WriteLine("Setting 'option2' to 'value2'");
            MyAppContext.Config.SetOption("option2", "value2");

            Console.WriteLine(string.Format("The value of 'option1' is {0}", MyAppContext.Config.GetOption("option1")));

            Console.WriteLine(string.Format("Value of 'weird_option' with a default value of 'be_safe': {0}", MyAppContext.Config.GetOption("weird_option", "be_safe")));

            Console.WriteLine("Saving the configuration");
            MyAppContext.Config.SaveConfiguration();

            Console.WriteLine("Done");
            Console.WriteLine("Press the ENTER key to continue...");
            Console.ReadLine();

        }
    }
}
