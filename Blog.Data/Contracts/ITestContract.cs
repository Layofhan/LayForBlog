using Blog.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Contracts
{
    public interface ITestContract:IDependency
    {
         string CodeTest();
    }
}
