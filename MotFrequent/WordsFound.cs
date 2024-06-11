using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotFrequent
{
    internal class WordsFound
    {
        private string _name;
        private int _count;

        public WordsFound (string name, int count)
        {
            this._name = name;
            this._count = count;
        }

        public string getName()
        {
            return _name;
        }
        public void setName(string name)
        {
            this._name = name;
        }

        public int getCount()
        {
            return _count;
        }
        public void setCount(int count)
        {
            this._count = count;
        }

    }
}
