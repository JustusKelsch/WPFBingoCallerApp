using BingoCallerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BingoCallerLibrary {
    public interface ISaveWinner {

        public void SaveWinner(PersonModel person);

        public void UpdateWins(PersonModel person);

        public bool ContainsPerson(PersonModel person);

        public ObservableCollection<PersonModel> GetAllWinners();

        public PersonModel GetPerson(int id);

    }
}
