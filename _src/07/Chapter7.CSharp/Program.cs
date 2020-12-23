
using System;
using System.Data.Entity;
using Chapter7.CSharp.Data;

namespace Chapter7.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            using (var context = new Context())
            {
                context.Database.Initialize(true);
                context.Database.Log = Console.Write;
            }
        }
    }
}
