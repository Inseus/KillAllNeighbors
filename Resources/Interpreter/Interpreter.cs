using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources
{
    public interface IExpression
    {
        void Interpret(Context context);
    }
       
    public class Interpreter : IExpression
    {
        public IExpression Expression1 { get; set; }

        public Interpreter()
        {
            Expression1 = new BackgroundInterpreter();
        }

        public void Interpret(Context context)
        {
            Console.WriteLine("Interpretuojama: " + context.Command);
            Expression1.Interpret(context);
        }
    }

    class BackgroundInterpreter : IExpression
    {
        public void Interpret(Context context)
        {
            string currentLine = new String(context.Command.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());
            string[] lineSplit = currentLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(lineSplit.Length <= 0)
            {
                context.Command = "Command was not understood. Try: background darkblue";
                return;
            }
            if (!lineSplit.Contains("background"))
            {
                context.Command = "Command was not understood. Try: background darkblue";
                return;
            }
            for (int i = 0; i < lineSplit.Length; i++)
            {
                Color newCol = Color.FromName(lineSplit[i]);
                if (newCol.ToArgb().Equals(0))
                    continue;
                else
                {
                    context.Command = "Background color was set to: " + lineSplit[i];
                    context.bgColor = newCol;
                    return;
                }
            }
            context.Command = "Command was not understood. Try: background darkblue";
        }
    }
}
