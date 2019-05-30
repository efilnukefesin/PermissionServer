using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using PermissionServer.Core.Factories;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionServer.CoreTests
{
    [TestClass]
    public class SubstitutionBuilderTests : BaseSimpleTest
    {
        #region SubstitutionBuilderProperties
        [TestClass]
        public class SubstitutionBuilderProperties : SubstitutionBuilderTests
        {

        }
        #endregion SubstitutionBuilderProperties

        #region SubstitutionBuilderConstruction
        [TestClass]
        public class SubstitutionBuilderConstruction : SubstitutionBuilderTests
        {

        }
        #endregion SubstitutionBuilderConstruction

        #region SubstitutionBuilderMethods
        [TestClass]
        public class SubstitutionBuilderMethods : SubstitutionBuilderTests
        {
            #region Build
            [TestMethod]
            public void Build()
            {
                var sourceUser = UserBuilder.CreateUser("Bob3").Build();
                var targetUser = UserBuilder.CreateUser("Bob4").Build();

                var result = SubstitutionBuilder.CreateSubstitution().SetSource(sourceUser).SetTarget(targetUser).SetValidity(new Validity(new DateTimeOffset(1999, 12, 31, 0, 0, 0, TimeSpan.FromHours(2)), new DateTimeOffset(DateTime.Now + TimeSpan.FromHours(1), TimeSpan.FromHours(2)))).Build();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Substitution));
                Assert.AreEqual(true, result.IsInPlace());
            }
            #endregion Build
        }
        #endregion SubstitutionBuilderMethods
    }
}
