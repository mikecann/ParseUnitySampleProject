using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseUnitySampleCommon.Models
{
    [ParseClassName("_User")]
    public class GameUser : ParseUser
    {       
        [ParseFieldName("playerName")]
        public string PlayerName
        {
            get { return GetProperty<string>("PlayerName"); }
            set { SetProperty<string>(value, "PlayerName"); }
        }

        public bool IsPlayerNameSet { get { return !String.IsNullOrEmpty(PlayerName); } }
    }
}
