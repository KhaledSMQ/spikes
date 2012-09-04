using System;

namespace MonadExperiments
{
	public class Maybe<A>
	{
		public static Maybe<A> Nothing = new Maybe<A>();

		public A Value { get; private set; }
		public bool HasValue { get; private set; }

		public Maybe(A a)
		{
			Value = a;
			HasValue = true;
		}

		private Maybe()
		{
			HasValue = false;
		}
	}

	public static class MaybeMonadExtensions
	{
		public static Maybe<A> MaybeUnit<A>(this A a)
		{
			return new Maybe<A>(a);
		}

		public static Maybe<B> Bind<A, B>(this Maybe<A> ma, Func<A, Maybe<B>> f)
		{
			if (!ma.HasValue)
				return Maybe<B>.Nothing;
			return f(ma.Value);
		}
	}

}
