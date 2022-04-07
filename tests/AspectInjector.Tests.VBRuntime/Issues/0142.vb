Imports System
Imports System.Collections.Generic
Imports AspectInjector.Broker
Imports AspectInjector.Tests.RuntimeAssets.CrossAssemblyHelpers

Namespace Issues
    Public Class Issue_0142_2
        Friend Interface IOne
        End Interface

        Friend Interface ITwo(Of T As IOne)
        End Interface

        Friend Interface IThree(Of T As IOne, U As ITwo(Of T))
        End Interface

        <Aspect(Scope.Global)>
        <Injection(GetType(MyAspect))>
        Public Class MyAspect
            Inherits Attribute

            <Advice(Kind.Around)>
            Public Function Before() As Object
                Return Nothing
            End Function
        End Class

        <MyAspect>
        Friend Class Class1
            Public Function MyFunction(Of T As IOne, U As ITwo(Of T), V As IThree(Of T, U))(ByVal generatorFunc1 As Func(Of V), ByVal generatorFunc2 As Func(Of Integer, IEnumerable(Of T)), ByVal generatorFunc3 As Func(Of String, T, U)) As V
                Throw New NotImplementedException()
            End Function
        End Class
    End Class

    Public Class Issue_0142
        <TestInjection>
        Public Class WierdContraintsClass(Of T As TestBaseClass(Of U), U As IAsyncResult)
            Inherits BaseGenericClass(Of T).NestedGenericClass(Of U).NestedGeneric2Class(Of String)

            Public Function [Get](Of G As BaseGenericClass(Of T).NestedGenericClass(Of U).NestedGeneric2Class(Of G))(ByVal data As T) As Tuple(Of U, G)
                Return Nothing
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(TestInjection))>
        <Mixin(GetType(BaseGenericClass(Of Integer).NestedGenericClass(Of String).NestedGeneric2Class(Of LocalDataStoreSlot).NestedGenericInterface(Of String)))>
        Public Class TestInjection
            Inherits Attribute
            Implements ICloneable, BaseGenericClass(Of Integer).NestedGenericClass(Of String).NestedGeneric2Class(Of LocalDataStoreSlot).NestedGenericInterface(Of String)

            Public Function Clone() As Object Implements ICloneable.Clone
                Throw New NotImplementedException()
            End Function

            Public Function GetH(Of tG As BaseGenericClass(Of Integer).NestedGenericClass(Of String).NestedGeneric2Class(Of LocalDataStoreSlot).NestedGenericInterface(Of J), J)(ByVal g As tG, ByVal i As LocalDataStoreSlot, ByVal u As String, ByVal t As Integer) As String Implements BaseGenericClass(Of Integer).NestedGenericClass(Of String).NestedGeneric2Class(Of LocalDataStoreSlot).NestedGenericInterface(Of String).GetH
                Throw New NotImplementedException()
            End Function

            <Advice(Kind.Before)>
            Public Sub Test()
            End Sub
        End Class
    End Class
End Namespace
