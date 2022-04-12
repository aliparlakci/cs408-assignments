﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs408_hw1_server
{
    public class Logger
    {
        private Action<string> _writer;

        public Logger()
        {
            _writer = (string log) =>
            {
                Console.WriteLine(log);
            };
        }

        public void Write(string log)
        {
            _writer(log);
        }

        public void SetWriter(Action<string> writer)
        {
            _writer = writer;
        }
    }
}
