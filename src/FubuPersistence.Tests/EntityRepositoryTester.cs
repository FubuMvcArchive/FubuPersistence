using System;
using System.Linq;
using FubuCore.Dates;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuPersistence.Tests
{
    [TestFixture]
    public class EntityRepositoryTester
    {
        [Test]
        public void when_deleting_an_entity_mark_the_entity_as_deleted()
        {
            var clock = SystemTime.AtLocalTime(DateTime.Today.AddHours(8));

            var repository = EntityRepository.InMemory(x => x.UseSystemTime(clock));
            var @case = new FakeEntity();
            @case.Deleted.ShouldBeNull();

            repository.Remove(@case);

            @case.Deleted.ShouldEqual(new Milestone(clock.UtcNow()));
        }


        [Test]
        public void can_still_find_a_soft_deleted_object()
        {
            var clock = SystemTime.AtLocalTime(DateTime.Today.AddHours(8));

            var repository = EntityRepository.InMemory(x => x.UseSystemTime(clock));
            var @case = new FakeEntity();
            @case.Deleted.ShouldBeNull();

            repository.Remove(@case);


            repository.Find<FakeEntity>(@case.Id).Id.ShouldEqual(@case.Id);
        }

        [Test]
        public void can_still_find_where_can_find_a_soft_deleted_object()
        {
            var clock = SystemTime.AtLocalTime(DateTime.Today.AddHours(8));

            var repository = EntityRepository.InMemory(x => x.UseSystemTime(clock));
            var @case = new FakeEntity();
            @case.Deleted.ShouldBeNull();

            repository.Remove(@case);


            repository.FindWhere<FakeEntity>(c => c.Id == @case.Id).Id.ShouldEqual(@case.Id);
        }

        [Test]
        public void soft_deleted_entities_are_not_available_from_All()
        {
            var clock = SystemTime.AtLocalTime(DateTime.Today.AddHours(8));

            var repository = EntityRepository.InMemory(x => x.UseSystemTime(clock));

            var c1 = new FakeEntity();
            var c2 = new FakeEntity();
            var c3 = new FakeEntity();
            var c4 = new FakeEntity();

            repository.Update(c1);
            repository.Update(c2);
            repository.Update(c3);
            repository.Update(c4);

            repository.Remove(c2);
            repository.Remove(c4);

            repository.All<FakeEntity>().ShouldHaveTheSameElementsAs(c1, c3);
        }


        [Test]
        public void assign_a_guid_if_one_does_not_exist()
        {
            var repository = EntityRepository.InMemory();
            var @case = new FakeEntity();

            @case.Id.ShouldEqual(Guid.Empty);

            repository.Update(@case);

            @case.Id.ShouldBeOfType<Guid>().ShouldNotEqual(Guid.Empty);
        }

        [Test]
        public void update_an_existing_case_should_replace_it()
        {
            var repository = EntityRepository.InMemory();
            var case1 = new FakeEntity();
            var case2 = new FakeEntity();

            repository.Update(case1);
            repository.All<FakeEntity>().Single().ShouldBeTheSameAs(case1);
            repository.Find<FakeEntity>(case1.Id).ShouldBeTheSameAs(case1);

            case2.Id = case1.Id;
            repository.Update(case2);

            repository.All<FakeEntity>().Single().ShouldBeTheSameAs(case2);
            repository.Find<FakeEntity>(case1.Id).ShouldBeTheSameAs(case2);
        }

        [Test]
        public void fetch_is_covariant_contravariant_you_know_what_I_mean()
        {
            var repository = EntityRepository.InMemory();
            var case1 = new FakeEntity();
            repository.Update(case1);

            repository.Find<FakeEntity>(case1.Id).ShouldBeTheSameAs(case1);
        }
    }
}