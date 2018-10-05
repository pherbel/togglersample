using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using TogglerService.Models;
using TogglerService.ViewModels;
using Xunit;

namespace TogglerService.Tests
{
    public class AutomapperTests : IClassFixture<AutomapperFixture>
    {
        private readonly AutomapperFixture _fixture;

        public AutomapperTests(AutomapperFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GlobalToggleVM_should_mapped_to_GlobalToggle_model()
        {
            GlobalToggleVM toggle = new GlobalToggleVM
            {
                Id = "isButtonBlue",
                Value = true,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };


            GlobalToggle result = _fixture.Mapper.Map<GlobalToggle>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);
            result.Created.Should().Be(toggle.Created);
            result.Modified.Should().Be(toggle.Modified);
        }

        [Fact]
        public void GlobalToggle_should_mapped_to_GlobalToggleVM_model()
        {
            GlobalToggle toggle = new GlobalToggle
            {
                Id = "isButtonBlue",
                Value = true,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };


            GlobalToggleVM result = _fixture.Mapper.Map<GlobalToggleVM>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);
            result.Created.Should().Be(toggle.Created);
            result.Modified.Should().Be(toggle.Modified);
        }

        [Fact]
        public void SaveGlobalToggleVM_should_mapped_to_GlobalToggle_model()
        {
            SaveGlobalToggleVM toggle = new SaveGlobalToggleVM
            {
                Id = "isButtonBlue",
                Value = true,

            };


            GlobalToggle result = _fixture.Mapper.Map<GlobalToggle>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);

        }

        [Fact]
        public void ServiceToggleVM_should_mapped_to_ServiceToggle_model()
        {
            ServiceToggleVM toggle = new ServiceToggleVM
            {
                Id = "isButtonBlue",
                Value = true,
                ServiceId = "ABC",
                VersionRange = "*",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };


            ServiceToggle result = _fixture.Mapper.Map<ServiceToggle>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);
            result.ServiceId.Should().Be(toggle.ServiceId);
            result.VersionRange.Should().Be(toggle.VersionRange);
            result.Created.Should().Be(toggle.Created);
            result.Modified.Should().Be(toggle.Modified);
        }

        [Fact]
        public void GlobaToggle_should_mapped_to_GlobalToggleVM_model()
        {
            ServiceToggle toggle = new ServiceToggle
            {
                Id = "isButtonBlue",
                Value = true,
                ServiceId = "ABC",
                VersionRange = "*",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };


            ServiceToggleVM result = _fixture.Mapper.Map<ServiceToggleVM>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);
            result.ServiceId.Should().Be(toggle.ServiceId);
            result.VersionRange.Should().Be(toggle.VersionRange);
            result.Created.Should().Be(toggle.Created);
            result.Modified.Should().Be(toggle.Modified);
        }

        [Fact]
        public void SaveServiceToggleVM_should_mapped_to_ServiceToggle_model()
        {
            SaveServiceToggleVM toggle = new SaveServiceToggleVM
            {
                Id = "isButtonBlue",
                Value = true,
                ServiceId = "ABC",
                VersionRange = "*",
            };


            ServiceToggle result = _fixture.Mapper.Map<ServiceToggle>(toggle);

            result.Id.Should().Be(toggle.Id);
            result.Value.Should().Be(toggle.Value);
            result.ServiceId.Should().Be(toggle.ServiceId);
            result.VersionRange.Should().Be(toggle.VersionRange);

        }

    }

    public class AutomapperFixture : IDisposable
    {

        public IMapper Mapper { get; set; }

        public AutomapperFixture()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper(typeof(Startup));
            ServiceProvider provider = services.BuildServiceProvider();
            Mapper = provider.GetService<IMapper>();
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
        public void Dispose()
        {
            Mapper = null;
        }
    }

}
