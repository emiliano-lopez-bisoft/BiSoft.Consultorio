using BiSoft.Consultorio.Dominio.Entidades;
using System.Globalization;

namespace BiSoft.Consultorio.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Act
            var doctor = new Doctor("Juan Perez", "Cardiologia");
            //Assert
            Assert.Equal("Juan Perez", doctor.Nombre);
            Assert.Equal("Cardiologia", doctor.Especialidad);
            Assert.NotEqual(Guid.Empty, doctor.Id);
            Assert.True(doctor.Nombre.Length > 5);
            Assert.True(doctor.Nombre.Contains(' '));
            Assert.True(doctor.Nombre.Length < 50);

        }

        [Theory]
        [InlineData("JuanPerez", "Cardiologia")]
        [InlineData("", "Pediatría")]
        [InlineData("Ana Perez", "")]
        [InlineData("An a", "Pediatría")]
        [InlineData("jsjskdkjdjssiueuejsjsjslaljsn ajslasnsjjsaljjaljsjajsjajsjajajsjjjaoiueijjsjsjsjsjsjiuesjs", "General")]
        public void IncorrectData(string nombre, string especialidad)
        {
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => new Doctor(nombre, especialidad));


        }

        [Fact]
        public void Test2()
        {
            //Arrange
            var doctor = new Doctor("Juan Perez", "Cardiologia");
            //Act
            doctor.Actualizar("Ejemplo Si", "Pediatría");
            //Assert
            Assert.Equal("Ejemplo Si", doctor.Nombre);
            Assert.Equal("Pediatría", doctor.Especialidad);
        }

        [Fact]
        public void Actualizar()
        {
            //Arrange
            var doctor = new Doctor("Juan Perez", "Cardiologia");
            //act
            doctor.Actualizar("Ejemplo Si", "Pediatría");
            //Assert
            Assert.Equal("Ejemplo Si", doctor.Nombre);
            Assert.Equal("Pediatría", doctor.Especialidad);
        }


        [Theory]
        [InlineData ("JuanPerez")]
        [InlineData("")]
        [InlineData("An a")]
        [InlineData("jsjskdkjdjssiueuejsjsjslaljsn ajslasnsjjsaljjaljsjajsjajsjajajsjjjaoiueijjsjsjsjsjsjiuesjs")]
        public void IncorrectNamePaciente(string nombre)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => new Paciente(nombre));
        }

        [Fact]
        public void ActualizarPaciente()
        {
            //Arrange
            var paciente = new Paciente("Juan Perez");
            //Act
            paciente.Actualizar("Ejemplo Si");
            //Assert
            Assert.Equal("Ejemplo Si", paciente.Nombre);
        }
    }
}
