Imports System
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.Text
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class AroundTests
        <Fact>
        Public Sub Advices_InjectAroundMethod_StructResult()
            Passed = False
            Dim a = New AroundTests_Target()
            Dim ref1 As Object = New Object()
            Dim ref2 = 2
            Dim str = ""
            Dim dt = Date.Now
            Dim arr = {1, 2, 3}
            Dim sb = New StringBuilder()
            Dim out1 As Object = Nothing, out2 As Integer = Nothing
            a.Do1(New Object(), 1, str, str, dt, dt, sb, sb, arr, arr, ref1, out1, ref2, out2, False, False)
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAroundMethod()
            Passed = False
            Dim a = New AroundTests_Target()
            Dim ref1 As Object = New Object()
            Dim ref2 = 2
            Dim out1 As Object = Nothing, out2 As Integer = Nothing
            a.Do2(New Object(), 1, ref1, out1, ref2, out2, False, False)
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAroundMethod_ModifyArguments()
            Dim i = 1
            Passed = True
            Call New AroundTests_ArgumentsModificationTarget().Fact(i)
            Assert.True(Passed)
        End Sub

        Friend Class AroundTests_Target
            <AroundTests_Aspect1>
            <AroundTests_Aspect2> 'fire first
            Public Function Do2(ByVal data As Object, ByVal value As Integer, ByRef testRef As Object, <Out> ByRef testOut As Object, ByRef testRefValue As Integer, <Out> ByRef testOutValue As Integer, ByVal passed As Boolean, ByVal passed2 As Boolean) As Object
                Checker.Passed = passed AndAlso passed2
                testOut = New Object()
                testOutValue = 1
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            <AroundTests_Aspect2> 'fire first
            Public Function Do1(Of T As ISerializable)(ByVal data As Object, ByVal value As Integer, ByVal str As String, ByRef rstr As String, ByVal dt As Date, ByRef rdt As Date, ByVal g As T, ByRef rg As T, ByVal arr As Integer(), ByRef rarr As Integer(), ByRef testRef As Object, <Out> ByRef testOut As Object, ByRef testRefValue As Integer, <Out> ByRef testOutValue As Integer, ByVal passed As Boolean, ByVal passed2 As Boolean) As T
                Dim test = New Object() {dt, rdt, arr, rarr, g, rg}
                dt = CDate(test(0))
                rdt = CDate(test(1))
                arr = CType(test(2), Integer())
                rarr = CType(test(3), Integer())
                g = CType(test(4), T)
                rg = CType(test(5), T)
                Checker.Passed = passed AndAlso passed2
                testOut = New Object()
                testOutValue = 1
                Return g
            End Function

            <AroundTests_Aspect1>
            Public Function [Object](ByVal data As Object) As Object
                Passed = True
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function ObjectRef(ByRef data As Object) As Object
                Passed = True
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function ObjectOut(<Out> ByRef data As Object) As Object
                Passed = True
                Return CSharpImpl.__Assign(data, New Object())
            End Function

            <AroundTests_Aspect1>
            Public Function Value(ByVal data As Integer) As Object
                Passed = True
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function ValueRef(ByRef data As Integer) As Object
                Passed = True
                Dim a = New Object() {1}
                data = CInt(a(0))
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function ValueOut(<Out> ByRef data As Integer) As Object
                Passed = True
                data = 1
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function ValueBoxed(ByVal data As Integer) As Object
                Passed = True
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function TypedObjectRef(ByRef data As StrongNameKeyPair) As Object
                Passed = True
                Dim a = New Object() {Nothing}
                data = CType(a(0), StrongNameKeyPair)
                Return New Object()
            End Function

            <AroundTests_Aspect1>
            Public Function GenericRef(Of T)(ByRef data As T) As T
                Passed = True
                Dim a = New Object() {Nothing}
                data = CType(a(0), T)
                Return data
            End Function

            <AroundTests_Aspect1>
            Public Function GenericOut(Of T)(<Out> ByRef data As T) As T
                Passed = True
                data = Nothing
                Return data
            End Function

            <AroundTests_Aspect1>
            Public Function Generic(Of T)(ByVal data As T) As T
                Passed = True
                data = Nothing
                Return data
            End Function

            Private Class CSharpImpl
                <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AroundTests_Simple))>
        Public Class AroundTests_Simple
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
                Return target(arguments)
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AroundTests_Aspect1))>
        Public Class AroundTests_Aspect1
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
                Return target(arguments.Take(arguments.Length - 2).Concat(New Object() {True, arguments.Last()}).ToArray())
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AroundTests_Aspect2))>
        Public Class AroundTests_Aspect2
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
                Return target(arguments.Take(arguments.Length - 1).Concat(New Object() {True}).ToArray())
            End Function
        End Class

        Friend Class AroundTests_ArgumentsModificationTarget
            <AroundTests_ArgumentsModificationAspect>
            Public Sub Fact(ByRef i As Integer)
                If i = 2 Then i = 3
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AroundTests_ArgumentsModificationAspect))>
        Public Class AroundTests_ArgumentsModificationAspect
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Arguments)> ByVal args As Object(),
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object)) As Object
                args(0) = 2
                Dim result = target(args)
                Passed = CInt(args(0)) = 3
                Return result
            End Function
        End Class
    End Class
End Namespace
