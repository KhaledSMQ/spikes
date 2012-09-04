using System;
using MonadExperiments;

namespace MonadExperiments
{
	public class Identity<A>
	{
		public A Value { get; private set; }

		public Identity(A a)
		{
			Value = a;
		}
	}

	public static class IdentityMonadExtensions
	{
		public static Identity<A> IdentityUnit<A>(this A a)
		{
			return new Identity<A>(a);
		}

		public static Identity<B> Bind<A, B>(this Identity<A> ma, Func<A, Identity<B>> f)
		{
			return f(ma.Value);
		}
	}
}
