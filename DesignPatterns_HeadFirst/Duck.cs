using System;

namespace DesignPatterns_HeadFirst
{
    public abstract class Duck
    {
        public FlyBehavior flyBehavior;
        public QuackBehavior quackBehavior;

        public abstract void Display();

        public void performQuack()
        {
            quackBehavior.quack();
        }
        public void performFly()
        {
            flyBehavior.fly();
        }
        public void swim()
        {
            Console.Write("All ducks can fly!");
        }
    }

    class MallardDuck : Duck
    {
        public MallardDuck()
        {
            quackBehavior = new Quack();
            flyBehavior = new Fly();
        }

        public override void Display()
        {
            Console.WriteLine("I'm a MallardDuck!");
        }

    }


    public interface QuackBehavior
    {
        void quack();
    }
    public class Squeack : QuackBehavior
    {
        public void quack()
        {
            Console.WriteLine("Squeack!");
        }
    }
    public class MuteQuack : QuackBehavior
    {
        public void quack()
        {
            Console.WriteLine(".. silence ..");
        }
    }
    public class Quack : QuackBehavior
    {
        public void quack()
        {
            Console.WriteLine("Qack!");
        }
    }

    public interface FlyBehavior
    {
        void fly();
    }
    class Fly : FlyBehavior
    {
        public void fly()
        {
            System.Console.WriteLine("Fly, fly!");
        }
    }
}
