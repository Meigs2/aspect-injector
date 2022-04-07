Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Issues
    Public Class Issue_0098
        <Fact>
        Public Sub Fixed()
            Dim temp = New Target().Variable
        End Sub

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub Before()
                Console.WriteLine("Before")
            End Sub

            <Advice(Kind.After)>
            Public Sub After()
                Console.WriteLine("After")
            End Sub
        End Class

        Private Class Target
            Private _variable As Object = Nothing

            <TestAspect>
            Public Property Variable As Object
                Get

                    Try
                        Return _variable
                    Catch __unusedException1__ As Exception
                        Throw
                    End Try
                End Get
                Set(ByVal value As Object)
                    Dim oldValue = _variable
                    Dim newValue = value

                    Try
                        _variable = value

                        If oldValue IsNot Nothing AndAlso oldValue IsNot newValue Then
                            LocalMethod(Me, oldValue)
                        End If

                    Catch __unusedException1__ As Exception
                        _variable = oldValue
                        Throw
                    End Try
                End Set
            End Property

            Private Sub LocalMethod(ByVal o1 As Object, ByVal o2 As Object)
            End Sub
        End Class
    End Class
End Namespace
