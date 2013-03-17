using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindANewman.Data.Repositories;
using FindANewman.Data.Repositories.Generic;
using FindANewman.Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using Rhino.Mocks;

namespace FindANewman.Data.Tests.Repositories.TestRepository
{
    public abstract class WhenTestingTheClass
    {
        protected IRepository<TestEntity> ClassToTest;

        protected IDataExecutionContext<TestEntity> DataExecutionContext { get; set; }

        protected TestEntity TestEntity { get; set; }

        protected DetachedCriteria DetachedCriteria { get; set; }

        protected ISession Session { get; set; }

        protected ICriteria CriteriaImplementation { get; set; }

        protected void Setup()
        {
            TestEntity = new TestEntity();

            Session = MockRepository.GenerateMock<ISession>();

            DetachedCriteria = DetachedCriteria.For<TestEntity>();

            CriteriaImplementation = MockRepository.GenerateMock<ICriteria>();
            DataExecutionContext = MockRepository.GenerateMock<IDataExecutionContext<TestEntity>>();

            DataExecutionContext.Stub(context => context.GetDetachedCriteria()).Return(DetachedCriteria);

            DataExecutionContext.Stub(context => context.ExecuteCriteria(Session, DetachedCriteria))
                .Return(CriteriaImplementation);

            CriteriaImplementation.Stub(criteria => criteria.List<TestEntity>()).Return(new List<TestEntity>());

            ClassToTest = new Repository<TestEntity>(DataExecutionContext);

        }
    }

    public class TestEntity : EntityBase
    {
    }
}
