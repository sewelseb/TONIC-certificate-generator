using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Options;

namespace DataAccessLayer
{
    public class InputParser : IInputParser
    {
        private string[] _argsList;
        public string SourcePath { get; set; }
        public string TemplatePath { get; set; }


        public InputParser(string[] argsList)
        {
            this._argsList = argsList;
        }

        public void Parse()
        {
            {
                OptionSet p = new OptionSet()
                {
                    {"s|source=", "File path of the data source.", v => this.SourcePath = v},
                    {"tp|template=", "File path of the template", v => this.TemplatePath = v}
                };
            }

        }
    }
}