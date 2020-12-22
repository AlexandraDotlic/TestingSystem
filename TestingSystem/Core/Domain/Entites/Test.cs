using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain.Entites.Questions;

namespace Core.Domain.Entites
{
    class Test
    {
        public short Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<BaseQuestion> Questions { get; private set; }
        public short GroupId { get; private set;}

        public Test(short _Id, string _Title, short _GroupId, ICollection<BaseQuestion> _Questions) 
        {
            Id = _Id;
            Title = _Title;
            GroupId = _GroupId;
            Questions = _Questions;
        }

        public Test()
        {

        }
    }
}
