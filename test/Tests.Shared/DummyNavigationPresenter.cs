using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Shared
{
    public class DummyNavigationPresenter : INavigationPresenter
    {
        public bool IsPresenterRegistered => throw new NotImplementedException();

        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTimeOffset CreationDate => throw new NotImplementedException();

        public int CreationIndex => throw new NotImplementedException();

        public IBaseObject CreationPredecessor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBaseObject CreationSucessor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public bool DiffersFromMemory()
        {
            throw new NotImplementedException();
        }

        public bool Present(string ViewUri, object DataContext)
        {
            throw new NotImplementedException();
        }

        public void RegisterPresenter(object Presenter)
        {
            throw new NotImplementedException();
        }

        public void Restore()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
