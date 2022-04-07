Imports System
Imports System.IO
Imports AspectInjector.Broker
Imports Xunit

Namespace Interfaces
    Public Class GenericInterfacesTests
        <Fact>
        Public Sub Interfaces_OpenGenericMethod()
            Dim ti = CType(New GenericMethodTestClass(), ITestInterface)
            Dim r1 = ti.[Get]("data")
            Assert.Equal("datawashere", r1)
            Dim r2 = ti.[Get](1)
            Assert.Equal(2, r2)
        End Sub

        <MyAspect>
        Public Class GenericMethodTestClass
            Public Sub New()
            End Sub
        End Class

        Public Interface ITestInterface
            Function [Get](Of [TO])(ByVal data As [TO]) As [TO]
        End Interface

        <Mixin(GetType(ITestInterface))>
        <Aspect(Scope.Global)>
        <Injection(GetType(MyAspect))>
        Public Class MyAspect
            Inherits Attribute
            Implements ITestInterface

            Private Function [Get](Of [TO])(ByVal data As [TO]) As [TO] Implements ITestInterface.[Get]
                If TypeOf data Is String Then Return CType(CObj((data.ToString() & "washere")), [TO])
                Return CObj(CInt(CObj(data)) + 1)
            End Function
        End Class
    End Class

    Public Class GenericInterfacesTests3
        <Fact>
        Public Sub Interfaces_OpenGenericMethodInClosedGenericType()
            Dim data4 = "ref"
            Dim ti = CType(New OpenGenericTestClass(), IfaceWrapClass(Of String).ITestInterface(Of StreamReader))
            Dim r1 = ti.[Get]("data", "123", New StreamReader(New MemoryStream()), data4)
            Assert.Equal("data123washereref", r1)
            Dim r2 = ti.[Get](1, "123", New StreamReader(New MemoryStream()), data4)
            Assert.Equal("5", r2)
        End Sub

        <MyAspect>
        Public Class OpenGenericTestClass
            Public Sub New()
            End Sub
        End Class

        Public Class IfaceWrapClass(Of TH As Class)
            Public Interface ITestInterface(Of T1 As TextReader)
                Inherits ITestInterface2(Of TH, T1)
            End Interface

            Public Interface ITestInterface2(Of T1 As TH, T2)
                Function [Get](Of [TO])(ByVal data As [TO], ByVal data2 As T1, ByVal data3 As T2, ByRef data4 As String) As T1
            End Interface
        End Class

        <Mixin(GetType(IfaceWrapClass(Of String).ITestInterface(Of StreamReader)))>
        <Aspect(Scope.Global)>
        <Injection(GetType(MyAspect))>
        Public Class MyAspect
            Inherits Attribute
            Implements IfaceWrapClass(Of String).ITestInterface(Of StreamReader)

            Public Function [Get](Of [TO])(ByVal data As [TO], ByVal data2 As String, ByVal data3 As StreamReader, ByRef data4 As String) As String Implements IfaceWrapClass(Of String).ITestInterface2(Of String, StreamReader).[Get]
                If TypeOf data Is String Then Return (data.ToString() & data2 & "washere" & data4)
                Return (CInt(CObj(data)) + 1 + data2.Length).ToString()
            End Function
        End Class
    End Class
End Namespace
