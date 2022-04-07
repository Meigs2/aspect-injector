Imports AspectInjector.Tests.Assets
Imports Xunit

Namespace General
    Public Class ReferencesTest
        <Fact>
        Public Sub NoRef_Runtime_To_PrivateCoreLib()
            Dim refs = GetType(ReferencesTest).Assembly.GetReferencedAssemblies()
            Assert.DoesNotContain(refs, Function(r) Equals(r.Name, "System.Private.CoreLib"))
        End Sub

        <Fact>
        Public Sub NoRef_RuntimeAssets_To_PrivateCoreLib()
            Dim refs = GetType(TestLog).Assembly.GetReferencedAssemblies()
            Assert.DoesNotContain(refs, Function(r) Equals(r.Name, "System.Private.CoreLib"))
        End Sub
    End Class
End Namespace
