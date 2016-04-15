using System;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Web;
using SASTokenCreator.Core;
using CommandLineParser;
using CommandLineParser.Arguments;

namespace SASTokenCreator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string _resourceUri = string.Empty;
            string _keyName = string.Empty;
            string _key = string.Empty;
            int _expiryPeriod = 0;

            CommandLineParser.CommandLineParser parser = new CommandLineParser.CommandLineParser();
            parser.ShowUsageOnEmptyCommandline = true;

            ValueArgument<string> resourceUri = new ValueArgument<string>('u', "resourceUri", "Set the URI to the SB resource");
            resourceUri.Optional = false;

            ValueArgument<string> keyName = new ValueArgument<string>('n',"keyName", "The name of the SAS key");
            keyName.Optional = false;

            ValueArgument<string> key = new ValueArgument<string>('k', "key", "The SAS key value");
            key.Optional = false;

            ValueArgument<int> expiryPeriod = new ValueArgument<int>('e', "expiryPeriod", "The expiry period, in seconds");
            expiryPeriod.Optional = false;

            parser.Arguments.Add(resourceUri);
            parser.Arguments.Add(keyName);
            parser.Arguments.Add(key);
            parser.Arguments.Add(expiryPeriod);

            parser.ParseCommandLine(args);

            if (resourceUri.Parsed)
            {
                _resourceUri = resourceUri.Value;
            }

            if (keyName.Parsed)
            {
                _keyName = keyName.Value;
            }

            if (key.Parsed)
            {
                _key = key.Value;
            }

            if (expiryPeriod.Parsed)
            {
                _expiryPeriod = expiryPeriod.Value;
            }

            TokenGenerator tokenGenerator = new TokenGenerator();
            string sasToken = tokenGenerator.CreateServiceBusSASToken(_resourceUri, _keyName, _key, _expiryPeriod);

            if (args.Length > 0)
            {
                Console.WriteLine(sasToken);
            }

            Console.WriteLine();
            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }

    }
}