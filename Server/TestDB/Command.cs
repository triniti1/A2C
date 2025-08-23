using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    interface ICommand
    {
        bool Execute();
    }


    public class CreateTablesCommand : ICommand
    {
        private readonly UsersTablesFactory _tablesFactory;
        public CreateTablesCommand(UsersTablesFactory tablesFactory)
        {
            _tablesFactory = tablesFactory;
        }
        public bool Execute()
        {
            if (_tablesFactory.CreateUsersTables())
            {
                Console.WriteLine("Users tables exist or created successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to create Users tables");
                return false;
            }
        }
    }

    public class CreateDataCommand : ICommand
    {
        private readonly UsersDataFactory _dataFactory;
        public CreateDataCommand(UsersDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
        }
        public bool Execute()
        {
            if (_dataFactory.CreateUsersData())
            {
                Console.WriteLine("Users data created successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Failed to create Users data");
                return false;
            }
        }
    }
}

