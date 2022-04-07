Imports AspectInjector.Tests.Assets
Imports AspectInjector.Tests.RuntimeAssets.CrossAssemblyHelpers
Imports Xunit

Namespace Issues
    Public Class Issue_0097
        <Fact>
        Public Sub Fixed()
            Dim temp = New Target().Value = 12
        End Sub

        Private Class SuperTargetBase(Of T)
            Inherits TestBaseClass(Of T)

            <TestAspect>
            Public Property Number As Integer
        End Class

        Private Class TargetBase(Of T)
            Inherits SuperTargetBase(Of T)

            <TestAspect>
            Public Property Text As String
        End Class

        Private Class Target
            Inherits TargetBase(Of Asset1)

            <TestAspect>
            Public Property Value As Double

            Private Sub Control()
                Dim a = Text
                Dim b = Number
            End Sub
        End Class
    End Class
End Namespace
