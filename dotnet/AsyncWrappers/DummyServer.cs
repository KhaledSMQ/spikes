using System.Reflection;
using System.Threading;

namespace AsyncWrappers
{
    public class DummyServer
    {
        public int Execute(int v)
        {
            Thread.Sleep(1000);

            if (v == 314)
                throw new SpecificException("we got a specific exception!");
            if (v == 271)
                throw new GeneralException("we got a general exception!");

            return v + 100;
        }

        public int Execute(int v, int w)
        {
            Thread.Sleep(1000);

            if (v == 314 && w == 271)
                throw new SpecificException("we got a specific exception!");
            if (v == 271 && w == 314)
                throw new GeneralException("we got a general exception!");
            
            return v + w + 100;
        }
    }
}
