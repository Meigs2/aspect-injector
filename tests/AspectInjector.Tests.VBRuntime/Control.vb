Imports System
Imports System.Runtime.InteropServices
Imports AspectInjector.Tests.Assets


Friend Class ControlWrapper(Of T1)
    Friend Class Control(Of T2)
        Implements IAssetIface1Wrapper(Of Asset2).IAssetIface1(Of Asset1), IAssetIface1Wrapper(Of Asset1).IAssetIface1(Of Asset2)

        Private Property TestProperty1 As Tuple(Of Asset2, Asset1) Implements IAssetIface1Wrapper(Of Asset2).IAssetIface1(Of Asset1).TestProperty
            Get
                Return CSharpImpl.__Throw(Of Object)(New NotImplementedException())
            End Get
            Set(ByVal value As Tuple(Of Asset2, Asset1))
                Throw New NotImplementedException()
            End Set
        End Property

        Private Property TestProperty2 As Tuple(Of Asset1, Asset2) Implements IAssetIface1Wrapper(Of Asset1).IAssetIface1(Of Asset2).TestProperty
            Get
                Return CSharpImpl.__Throw(Of Object)(New NotImplementedException())
            End Get
            Set(ByVal value As Tuple(Of Asset1, Asset2))
                Throw New NotImplementedException()
            End Set
        End Property

        Private Custom Event TestEvent1 As EventHandler(Of Tuple(Of Asset2, Asset1)) Implements IAssetIface1Wrapper(Of Asset2).IAssetIface1(Of Asset1).TestEvent
            AddHandler(ByVal value As EventHandler(Of Tuple(Of Asset2, Asset1)))
                Throw New NotImplementedException()
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of Tuple(Of Asset2, Asset1)))
                Throw New NotImplementedException()
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As Tuple(Of Asset2, Asset1))
            End RaiseEvent
        End Event

        Private Custom Event TestEvent2 As EventHandler(Of Tuple(Of Asset1, Asset2)) Implements IAssetIface1Wrapper(Of Asset1).IAssetIface1(Of Asset2).TestEvent
            AddHandler(ByVal value As EventHandler(Of Tuple(Of Asset1, Asset2)))
                Throw New NotImplementedException()
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of Tuple(Of Asset1, Asset2)))
                Throw New NotImplementedException()
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As Tuple(Of Asset1, Asset2))
            End RaiseEvent
        End Event

        Private Sub EmptyMethod1() Implements IAssetIface1Wrapper(Of Asset2).IAssetIface1(Of Asset1).EmptyMethod
            Throw New NotImplementedException()
        End Sub

        Private Sub EmptyMethod2() Implements IAssetIface1Wrapper(Of Asset1).IAssetIface1(Of Asset2).EmptyMethod
            Throw New NotImplementedException()
        End Sub

        Private Function TestMethod1(Of T3)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As Asset2, ByVal a4 As Asset1, ByVal a5 As T3, ByRef ar1 As Integer, ByRef ar2 As Asset1, ByRef ar3 As Asset2, ByRef ar4 As Asset1, ByRef ar5 As T3, <Out> ByRef ao1 As Integer, <Out> ByRef ao2 As Asset1, <Out> ByRef ao3 As Asset2, <Out> ByRef ao4 As Asset1, <Out> ByRef ao5 As T3) As Tuple(Of Integer, Asset1, Asset2, Asset1, T3) Implements IAssetIface1Wrapper(Of Asset2).IAssetIface1(Of Asset1).TestMethod
            Throw New NotImplementedException()
        End Function

        Private Function TestMethod2(Of T3)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As Asset1, ByVal a4 As Asset2, ByVal a5 As T3, ByRef ar1 As Integer, ByRef ar2 As Asset1, ByRef ar3 As Asset1, ByRef ar4 As Asset2, ByRef ar5 As T3, <Out> ByRef ao1 As Integer, <Out> ByRef ao2 As Asset1, <Out> ByRef ao3 As Asset1, <Out> ByRef ao4 As Asset2, <Out> ByRef ao5 As T3) As Tuple(Of Integer, Asset1, Asset1, Asset2, T3) Implements IAssetIface1Wrapper(Of Asset1).IAssetIface1(Of Asset2).TestMethod
            Throw New NotImplementedException()
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal throw statements")>
            Shared Function __Throw(Of T)(ByVal e As Exception) As T
                Throw e
            End Function
        End Class
    End Class
End Class