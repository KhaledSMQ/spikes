using Autofac;

namespace WebApi6
{
    public class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new BusinessLogic()).As<IBusinessLogic>();
        }
    }
}