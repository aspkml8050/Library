using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.App_Code.CSharp
{
    public abstract class BaseMember
    {
        private string _firstName;

        private string _lastName;

        private string _contactNo;

        private string _emailAddress;

        private string _instName;

        private string _instShortName;

        private string _instCode;

        private string _userID;

        public string firstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                }
            }
        }

        public string lastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                }
            }
        }

        public string contactNo
        {
            get
            {
                return _contactNo;
            }
            set
            {
                if (value != _contactNo)
                {
                    _contactNo = value;
                }
            }
        }

        public string emailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                if (value != _emailAddress)
                {
                    _emailAddress = value;
                }
            }
        }

        public string instName
        {
            get
            {
                return _instName;
            }
            set
            {
                if (value != _instName)
                {
                    _instName = value;
                }
            }
        }

        public string instShortName
        {
            get
            {
                return _instShortName;
            }
            set
            {
                if (value != _instShortName)
                {
                    _instShortName = value;
                }
            }
        }

        public string instCode
        {
            get
            {
                return _instCode;
            }
            set
            {
                if (value != _instCode)
                {
                    _instCode = value;
                }
            }
        }

        public string userID
        {
            get
            {
                return _userID;
            }
            set
            {
                if (value != _userID)
                {
                    _userID = value;
                }
            }
        }
    }
}