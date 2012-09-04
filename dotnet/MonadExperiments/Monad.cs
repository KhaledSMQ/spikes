using System;

namespace MonadExperiments
{
	public abstract class Monad
	{
		public abstract class M<A>
		{
			public abstract A Value { get; }
		}

		public abstract M<A> Unit<A>(A a);
		public abstract M<B> Bind<A, B>(M<A> ma, Func<A, M<B>> f);
	}
}
