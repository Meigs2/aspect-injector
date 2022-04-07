Imports System
Imports System.Linq
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace General
    Public Class InjectionDataTests
        <Fact>
        Public Sub Injection_Parameters_Are_Correct()
            Dim a = New TestClass()
            Passed = False
            a.Do()
            Assert.True(Passed)
            Passed = False
            a.Do2()
            Assert.True(Passed)
        End Sub

        Private Class TestClass
            <TestInjection(True, Byte.MaxValue, SByte.MinValue, Char.MaxValue, Short.MinValue, UShort.MaxValue, Integer.MinValue, UInteger.MaxValue, Single.MinValue, Long.MinValue, ULong.MaxValue, Double.MinValue)>
            Public Sub [Do]()
            End Sub

            <TestInjection2(New ULong() {ULong.MinValue, ULong.MaxValue}, 2, New Object() {New Object() {0.5F, True}, 1, GetType(TestInjection2)}, New Double() {12R, -0.777R}, New Single() {}, New Boolean() {True}, GetType(TestClass), StringComparison.InvariantCulture)>
            Public Sub Do2()
            End Sub
        End Class

        <Injection(GetType(TestAspect))>
        Private Class TestInjection2
            Inherits Attribute

            Public Sub New(ByVal oa As Object())
            End Sub

            Public Sub New(ByVal ul As ULong(), ByVal o As Object, ByVal oa As Object(), ByVal da As Double(), ByVal fa As Single(), ByVal ba As Boolean(), ByVal type As Type, ByVal e As StringComparison)
                Me.Ul = ul
                Me.O = o
                Me.Oa = oa
                Me.Da = da
                Me.Fa = fa
                Me.Ba = ba
                Me.Type = type
                Me.E = e
            End Sub

            Public ReadOnly Property Ul As ULong()
            Public ReadOnly Property O As Object
            Public ReadOnly Property Oa As Object()
            Public ReadOnly Property Da As Double()
            Public ReadOnly Property Fa As Single()
            Public ReadOnly Property Ba As Boolean()
            Public ReadOnly Property Type As Type
            Public ReadOnly Property E As StringComparison
        End Class

        <Injection(GetType(TestAspect))>
        Private Class TestInjection
            Inherits Attribute

            Public Sub New(ByVal bo As Boolean, ByVal b As Byte, ByVal sb As SByte, ByVal c As Char, ByVal s As Short, ByVal us As UShort, ByVal i As Integer, ByVal ui As UInteger, ByVal f As Single, ByVal l As Long, ByVal ul As ULong, ByVal d As Double)
                Me.Bo = bo
                Me.B = b
                Me.Sb = sb
                Me.C = c
                Me.S = s
                Me.Us = us
                Me.I = i
                Me.Ui = ui
                Me.F = f
                Me.L = l
                Me.Ul = ul
                Me.D = d
            End Sub

            Public ReadOnly Property Bo As Boolean
            Public ReadOnly Property B As Byte
            Public ReadOnly Property Sb As SByte
            Public ReadOnly Property C As Char
            Public ReadOnly Property S As Short
            Public ReadOnly Property Us As UShort
            Public ReadOnly Property I As Integer
            Public ReadOnly Property Ui As UInteger
            Public ReadOnly Property F As Single
            Public ReadOnly Property L As Long
            Public ReadOnly Property Ul As ULong
            Public ReadOnly Property D As Double
        End Class

        <Aspect(Scope.Global)>
        Public Class TestAspect
            <Advice(Kind.Before)>
            Public Sub Before2(
            <Argument(Source.Triggers)> ByVal data As Attribute())
                Dim a = data.OfType(Of TestInjection2)().FirstOrDefault()
                If a Is Nothing Then Return
                Passed = a.Ul(0) = ULong.MinValue AndAlso a.Ul(1) = ULong.MaxValue AndAlso CInt(a.O) = 2 AndAlso CSng(CType(a.Oa(0), Object())(0)) = 0.5F AndAlso CBool(CType(a.Oa(0), Object())(1)) = True AndAlso CInt(a.Oa(1)) = 1 AndAlso CType(a.Oa(2), Type) Is GetType(TestInjection2) AndAlso a.Da(0) = 12R AndAlso a.Da(1) = -0.777R AndAlso a.Fa.Length = 0 AndAlso a.Ba(0) = True AndAlso a.Type Is GetType(TestClass) AndAlso a.E = StringComparison.InvariantCulture
            End Sub

            <Advice(Kind.Before)>
            Public Sub Before(
            <Argument(Source.Triggers)> ByVal data As Attribute())
                Dim a = data.OfType(Of TestInjection)().FirstOrDefault()
                If a Is Nothing Then Return
                Passed = a.Bo AndAlso a.B = Byte.MaxValue AndAlso a.Sb = SByte.MinValue AndAlso a.C = Char.MaxValue AndAlso a.S = Short.MinValue AndAlso a.Us = UShort.MaxValue AndAlso a.I = Integer.MinValue AndAlso a.Ui = UInteger.MaxValue AndAlso a.F = Single.MinValue AndAlso a.L = Long.MinValue AndAlso a.Ul = ULong.MaxValue AndAlso a.D = Double.MinValue
            End Sub
        End Class
    End Class

    Public Class CustomAttributesTests
        <Fact>
        Public Sub General_CustomAttributes_PassRoutableValues()
            Passed = False
            Dim a = New CustomAttributesTests_Target()
            a.Do()
            Assert.True(Passed)
            Dim b = New CustomAttributesTestsAttribute("111") With {
                .Value = "olo"
            }
        End Sub

        <Fact>
        Public Sub General_CustomAttributes_Multiple()
            Passed = False
            Dim a = New CustomAttributesTests_MultipleTarget()
            a.Do123()
            Assert.True(Passed)
        End Sub
    End Class

    <CustomAttributesTests("TestHeader", Value:="ololo", data:=43)>
    Friend Class CustomAttributesTests_Target
        Public Sub [Do]()
        End Sub
    End Class

    <CustomAttributesTests_Multiple1>
    Friend Class CustomAttributesTests_MultipleTarget
        <CustomAttributesTests_Multiple2>
        Public Sub Do123()
        End Sub
    End Class

    <Injection(GetType(CustomAttributesTests_Aspect))>
    Friend Class CustomAttributesTestsAttribute
        Inherits Attribute

        Private _Header As String

        Public Property Header As String
            Get
                Return _Header
            End Get
            Private Set(ByVal value As String)
                _Header = value
            End Set
        End Property

        Public Property Value As String
        Public data As Integer = 42

        Public Sub New(ByVal header As String)
            Me.Header = header
        End Sub
    End Class

    <Aspect(Scope.Global)>
    Public Class CustomAttributesTests_Aspect
        <Advice(Kind.After)>
        Public Sub AfterMethod(
        <Argument(Source.Triggers)> ByVal data As Attribute())
            Dim a = TryCast(data(0), CustomAttributesTestsAttribute)
            Passed = Equals(a.Header, "TestHeader") AndAlso Equals(a.Value, "ololo") AndAlso a.data = 43
        End Sub
    End Class

    <Injection(GetType(CustomAttributesTests_MultipleAspect))>
    Public Class CustomAttributesTests_Multiple1Attribute
        Inherits Attribute
    End Class

    <Injection(GetType(CustomAttributesTests_MultipleAspect))>
    Public Class CustomAttributesTests_Multiple2Attribute
        Inherits Attribute
    End Class

    <Aspect(Scope.Global)>
    Public Class CustomAttributesTests_MultipleAspect
        <Advice(Kind.After)>
        Public Sub AfterMethod(
        <Argument(Source.Triggers)> ByVal data As Attribute())
            Passed = data.Length = 2 AndAlso TypeOf data(0) Is CustomAttributesTests_Multiple1Attribute AndAlso TypeOf data(1) Is CustomAttributesTests_Multiple2Attribute
        End Sub
    End Class
End Namespace
