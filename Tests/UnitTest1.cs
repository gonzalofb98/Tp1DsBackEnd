using Dominio.Entidades;
using Moq;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCrearUnModelo()
        {
            var model = new Modelo(25,"air-fox",15,30,15,30);

            Assert.AreEqual(25, model.Sku);
            Assert.AreEqual("air-fox", model.Descripcion);
            Assert.AreEqual(15, model.LimiteInferiorReproceso);
            Assert.AreEqual(30, model.LimiteSuperiorReproceso);
            Assert.AreEqual(15, model.LimiteInferiorObservado);
            Assert.AreEqual(30, model.LimiteSuperiorObservado);
        }

        //[TestMethod]
        //public void TestEstadoDeLaOpAlCrearEsActiva()
        //{
        //    //moq
        //    var mockModelo = new Mock<Modelo>();
        //    var mockLinea = new Mock<LineaDeTrabajo>();
        //    var mockColor = new Mock<Color>();
        //    var mockEmpleado = new Mock<Empleado>();

        //    var Op = new OrdenDeProduccion("2323a", DateTime.Now,mockModelo.Object,mockColor.Object, 
        //        mockLinea.Object, mockEmpleado.Object);

        //    Assert.IsNotNull(Op);
        //    Assert.AreEqual(EstadoOp.ACTIVA, Op.Estado);
        //}

        //[TestMethod]
        //public void TestFinalizarUnaOrdenDeProduccion()
        //{
        //    var mockModelo = new Mock<Modelo>();
        //    var mockLinea = new Mock<LineaDeTrabajo>();
        //    var mockColor = new Mock<Color>();
        //    var mockEmpleado = new Mock<Empleado>();

        //    var Op = new OrdenDeProduccion("2323a", DateTime.Now, mockModelo.Object, mockColor.Object,
        //        mockLinea.Object, mockEmpleado.Object);

        //    Op.FinalizarOrden();

        //    Assert.IsNotNull(Op);
        //    Assert.AreEqual(EstadoOp.FINALIZADA, Op.Estado);
        //}


        //[TestMethod]
        //public void TestAgregarUnaIncidenciaAJornadaDeTipoDefecto()
        //{
        //    var mockModelo = new Mock<Modelo>();
        //    var mockLinea = new Mock<LineaDeTrabajo>();
        //    var mockColor = new Mock<Color>();
        //    var mockEmpleado = new Mock<Empleado>();
        //    var mockTurno = new Mock<Turno>();
        //    var mockDefecto = new Mock<Defecto>();

        //    var Op = new OrdenDeProduccion("2323a", DateTime.Now, mockModelo.Object, mockColor.Object,
        //        mockLinea.Object, mockEmpleado.Object);


        //    Op.EstablecerNuevaJornada(DateTime.Now,DateTime.Now.AddHours(8), mockTurno.Object);

        //    var jornada = Op.GetJornadaActual();

        //    jornada.AgregarIncidencia(DateTime.Now, Pie.IZQUIERDO, mockDefecto.Object);

        //    Assert.IsNotNull(jornada.Incidencias);
        //    Assert.IsTrue(jornada.Incidencias.Count() > 0);
        //}

        //[TestMethod]
        //public void TestAgregarUnaIncidenciaAJornadaDeTipoPrimera()
        //{
        //    var mockModelo = new Mock<Modelo>();
        //    var mockLinea = new Mock<LineaDeTrabajo>();
        //    var mockColor = new Mock<Color>();
        //    var mockEmpleado = new Mock<Empleado>();
        //    var mockTurno = new Mock<Turno>();

        //    var Op = new OrdenDeProduccion("2323a", DateTime.Now, mockModelo.Object, mockColor.Object,
        //        mockLinea.Object, mockEmpleado.Object);

        //    Op.EstablecerNuevaJornada(DateTime.Now, DateTime.Now.AddHours(8), mockTurno.Object);

        //    var jornada = Op.GetJornadaActual();

        //    jornada.AgregarIncidencia(DateTime.Now);

        //    Assert.IsNotNull(jornada.Incidencias);
        //    Assert.IsTrue(jornada.Incidencias.Count() > 0);
        //}

    }
}