using System;
using IdentityMonad = MonadExperiments.IdentityMonadExtensions;

namespace MonadExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new Program();
			p.UseIdentityMonad();
			p.UseMaybeMonad();
			Console.ReadLine();
		}

		private void UseIdentityMonad()
		{
			Console.WriteLine("Using IdentityMonad:");
			var x = 10;
			var y = 20;
			var r1 = x.IdentityUnit().Bind(
				s1 => y.IdentityUnit().Bind(
					t1 => (s1 + t1).IdentityUnit()));
			Console.WriteLine("{0} + {1} = {2}", x, y, r1.Value);
			x = 5;
			y = 7;
			var r2 = x.IdentityUnit().Bind(
				s2 => y.IdentityUnit().Bind(
					t2 => (s2 + t2).IdentityUnit()));
			Console.WriteLine("{0} + {1} = {2}", x, y, r2.Value);
		}

		private void UseMaybeMonad()
		{
			Console.WriteLine("Using MaybeMonad:");

			var x = "x";
			var y = 17;
			var z = new DateTime(2000, 1, 2, 3, 5, 6);
			var w = Maybe<object>.Nothing;
			
			var r1 = x.MaybeUnit().Bind(
				s1 => y.MaybeUnit().Bind(
					t1 => (string.Format("x = '{0}', y = '{1}'", x, y)).MaybeUnit())
				);
			Console.WriteLine("r1 = '{0}'", r1.Value);

			var r2 = x.MaybeUnit().Bind(
				s2 => y.MaybeUnit().Bind(
					t2 => z.MaybeUnit().Bind(
						u2 => (string.Format("x = '{0}', y = '{1}', z = '{2}'", x, y, z)).MaybeUnit())
					)
				);
			Console.WriteLine("r2 = '{0}'", r2.Value);

			var r3 = x.MaybeUnit().Bind(
				s3 => w.Bind(
					t3 => z.MaybeUnit().Bind(
						u3 => (string.Format("x = '{0}', w = '{1}', z = '{2}'", x, w, z)).MaybeUnit())
					)
				);
			Console.WriteLine("r3.HasValue = '{0}', r3 = '{1}'", r3.HasValue, r3.Value);
		}
	}
}
