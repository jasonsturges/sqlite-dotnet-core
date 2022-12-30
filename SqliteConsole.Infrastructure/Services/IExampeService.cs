using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteConsole.Infrastructure.Services
{
    public interface IExampleService
    {

        void GetExamples();
        void AddExample(string name);

    }
}