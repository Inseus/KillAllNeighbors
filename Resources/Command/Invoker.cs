using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KillAllNeighbors.Resources.Command
{
    public class Invoker
    {
        //private Receiver receiver = new Receiver();
        //private List<Command> commands = new List<Command>();
        //private int current = 0;

        //public void AddCommand(int x, int y, Player player)
        //{
        //    Command command = new ConcreteCommand(x,y,player);
        //    command.Execute();
        //    commands.Add(command);
        //}
        static Queue<Command> commandBuffer = new Queue<Command>();
        static List<Command> commandHistory = new List<Command>();
        static int counter;
        public static void AddCommand(Command command)
        {
            //if(counter < commandHistory.Count)
            //    while (commandHistory.Count > counter) //reset history on new moves
            //    {
            //        commandHistory.RemoveAt(counter);//pergreitai?
            //    }

            commandBuffer.Enqueue(command);
            if (commandBuffer.Count > 0)
            {
                Command c = commandBuffer.Dequeue();
                c.Execute();
                //commandBuffer.Dequeue().Execute();
                if(Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.D))
                {
                    commandHistory.Add(c);
                    counter++;
                }
                
            }
            
            if (Keyboard.IsKeyDown(Key.Z))
            {
                if(counter > 0)
                {
                    counter--;
                    commandHistory[counter].UnExecute();
                }
            }
            else if(Keyboard.IsKeyDown(Key.X))
            {
                if(counter < commandHistory.Count)
                {
                    commandHistory[counter].Execute();
                    counter++;
                }
            }
        }
    }
}