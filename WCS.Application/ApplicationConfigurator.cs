using Communication.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Application
{
    public static class ApplicationConfigurator
    {
        public static void AddBehaviorModule(MediatRServiceConfiguration configuration)
        {
            configuration.AddBehavior<ReadS7PlcPipelineBehavior>();
        }
    }
}
