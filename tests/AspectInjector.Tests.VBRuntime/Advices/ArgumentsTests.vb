Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class ArgumentsTests
        <Fact>
        Public Sub AdviceArguments_Name_Of_Real_Target()
            Passed = False
            Dim r = New ArgumentsTests_PropertyTarget().Fact
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Instance_InjectBeforeMethod_NotNull()
            Passed = False
            Call New ArgumentsTests_InstanceTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Instance_InjectBeforeStaticMethod_Null()
            Passed = False
            Call ArgumentsTests_StaticInstanceTarget.Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_ReturnType_InjectBeforeMethod()
            Passed = False
            Call ArgumentsTests_ReturnTypeTarget.Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectBeforeStaticMethod()
            Passed = False
            Call New ArgumentsTests_StaticMethodTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectBeforeStaticConstructor()
            Passed = False
            Dim temp = New ArgumentsTests_StaticConstructorTarget()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_ReturnValue_AfterMethod()
            Passed = False
            Dim temp = New ArgumentsTests_AroundRetValTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectBeforeMethod()
            Passed = False
            Call New ArgumentsTests_MethodTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectBeforeConstructor()
            Passed = False
            Dim temp = New ArgumentsTests_ConstructorTarget()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectBeforeConstructorChain()
            Passed = False
            Call New ArgumentsTests_ConstructorChainTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Method_InjectAroundMethod()
            Passed = False
            Call New ArgumentsTests_AroundMethodTarget().Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Arguments_InjectBeforeMethod()
            Dim obj = New Object()
            Dim outObj As Object
            Dim val = 1
            Passed = False
            Dim outVal As Integer = Nothing
            Call New ArgumentsTests_ArgumentsTarget().Fact(obj, obj, outObj, val, val, outVal)
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub AdviceArguments_Arguments_InjectBeforeStaticMethod()
            Passed = False
            ArgumentsTests_StaticArgumentsTarget.Fact(1, "2")
            Assert.True(Passed)
        End Sub

        Friend Class ArgumentsTests_InstanceTarget
            <ArgumentsTests_InstanceAspect>
            Public Sub Fact()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_InstanceAspect))>
        Public Class ArgumentsTests_InstanceAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod(
            <Argument(Source.Instance)> ByVal instance As Object)
                Passed = instance IsNot Nothing
            End Sub
        End Class

        Friend NotInheritable Class ArgumentsTests_StaticInstanceTarget
            <ArgumentsTests_StaticInstanceAspect>
            Public Shared Sub Fact()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_StaticInstanceAspect))>
        Public Class ArgumentsTests_StaticInstanceAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod(
            <Argument(Source.Instance)> ByVal instance As Object)
                Passed = instance Is Nothing
            End Sub
        End Class

        <ArgumentsTests_ReturnTypeAspect>
        Friend NotInheritable Class ArgumentsTests_ReturnTypeTarget
            Public Shared Sub Fact()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_ReturnTypeAspect))>
        Public Class ArgumentsTests_ReturnTypeAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod(
            <Argument(Source.Type)> ByVal type As Type)
                Passed = type Is GetType(ArgumentsTests_ReturnTypeTarget)
            End Sub
        End Class

        Friend Class ArgumentsTests_StaticMethodTarget
            <ArgumentsTests_StaticMethodAspect>
            Public Sub Fact()
            End Sub
        End Class

        Friend Class ArgumentsTests_StaticConstructorTarget
            <ArgumentsTests_StaticMethodAspect>
            Shared Sub New()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_StaticMethodAspect))>
        Public Class ArgumentsTests_StaticMethodAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method Or Target.Constructor)>
            Public Sub BeforeMethod(
            <Argument(Source.Metadata)> ByVal method As MethodBase)
                Passed = method IsNot Nothing
            End Sub
        End Class

        Friend Class ArgumentsTests_MethodTarget
            <ArgumentsTests_MethodAspect>
            Public Sub Fact()
            End Sub
        End Class

        Friend Class ArgumentsTests_ConstructorChainTarget
            Public Sub New()
            End Sub

            Public Sub New(ByVal a As Integer)
                Me.New()
            End Sub

            Public Sub New(ByVal a As Integer, ByVal b As Integer)
                Me.New(a)
            End Sub

            <ArgumentsTests_MethodAspect>
            Public Sub Fact()
            End Sub
        End Class

        <ArgumentsTests_MethodAspect>
        Friend Class ArgumentsTests_ConstructorTarget
            <ArgumentsTests_MethodAspect>
            Public Sub New()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_MethodAspect))>
        Public Class ArgumentsTests_MethodAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method Or Target.Constructor)>
            Public Sub BeforeMethod(
            <Argument(Source.Metadata)> ByVal method As MethodBase)
                Passed = method IsNot Nothing
            End Sub
        End Class

        Friend Class ArgumentsTests_ArgumentsTarget
            <ArgumentsTests_ArgumentsAspect>
            Public Sub Fact(ByVal obj As Object, ByRef objRef As Object, <Out> ByRef objOut As Object, ByVal value As Integer, ByRef valueRef As Integer, <Out> ByRef valueOut As Integer)
                valueOut = 1
                objOut = New Object()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_ArgumentsAspect))>
        Public Class ArgumentsTests_ArgumentsAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod(
            <Argument(Source.Arguments)> ByVal args As Object())
                Passed = args(0) IsNot Nothing AndAlso args(1) IsNot Nothing AndAlso args(2) Is Nothing AndAlso CInt(args(3)) = 1 AndAlso CInt(args(4)) = 1 AndAlso CInt(args(5)) = 0
            End Sub
        End Class

        Friend NotInheritable Class ArgumentsTests_StaticArgumentsTarget
            <ArgumentsTests_StaticArgumentsAspect>
            Public Shared Sub Fact(ByVal a As Integer, ByVal b As String)
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_StaticArgumentsAspect))>
        Public Class ArgumentsTests_StaticArgumentsAspect
            Inherits Attribute

            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod(
            <Argument(Source.Arguments)> ByVal args As Object())
                Passed = CInt(args(0)) = 1 AndAlso Equals(CStr(args(1)), "2")
            End Sub
        End Class

        Friend Class ArgumentsTests_AroundMethodTarget
            <ArgumentsTests_AroundMethodAspect>
            Public Sub Fact()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_AroundMethodAspect))>
        Public Class ArgumentsTests_AroundMethodAspect
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function BeforeMethod(
            <Argument(Source.Metadata)> ByVal method As MethodBase) As Object
                Passed = Equals(method.Name, "Fact")
                Return Nothing
            End Function
        End Class

        Friend Class ArgumentsTests_AroundRetValTarget
            <ArgumentsTests_AroundRetValAspect>
            Public Function Fact() As Object
                Return New Object()
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(ArgumentsTests_AroundRetValAspect))>
        Public Class ArgumentsTests_AroundRetValAspect
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub AfterMethod(
            <Argument(Source.ReturnValue)> ByVal ret As Object)
                Passed = ret IsNot Nothing
            End Sub
        End Class

        Friend Class ArgumentsTests_PropertyTarget
            <ArgumentsTests_PropertyTarget_Aspect>
            Public Property Fact As Object
        End Class

        <Injection(GetType(ArgumentsTests_PropertyTarget_Aspect))>
        <Aspect(Scope.Global)>
        Public Class ArgumentsTests_PropertyTarget_Aspect
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub TestName(
            <Argument(Source.Name)> ByVal name As String)
                Passed = Equals(name, "Fact")
            End Sub
        End Class
    End Class
End Namespace
