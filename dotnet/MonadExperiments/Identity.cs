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

	public static class IdentityMonad
	{
		public static Identity<A> Unit<A>(A a)
		{
			Console.WriteLine("Constructing a new Identity<{0}> with value '{1}'...", typeof(A), a);
			return new Identity<A>(a);
		}

		public static Identity<B> Bind<A, B>(Identity<A> ma, Func<A, Identity<B>> f)
		{
			Console.WriteLine("Binding Identity<{0}> with value '{1}' to function from '{2}' to Identity<{3}>...",
				typeof(A), ma.Value, typeof(A), typeof(B));
			return f(ma.Value);
		}
	}

	public static class IdentityMonadExtensions
	{
		public static Identity<A> IdentityUnit<A>(this A a)
		{
			return IdentityMonad.Unit(a);
		}

		public static Identity<B> Bind<A, B>(this Identity<A> ma, Func<A, Identity<B>> f)
		{
			return IdentityMonad.Bind(ma, f);
		}
	}
}
