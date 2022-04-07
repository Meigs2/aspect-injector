Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Issues
    Public Class Issue_0123
        <Fact>
        Public Sub Fixed()
            Passed = False
            Call New HomeController().ActionOnlyAdminsCanDo().Wait()
            Assert.True(Passed)
        End Sub

        Public Enum UserRole
            Guest
            Normal
            Admin
        End Enum

        Public Enum UserRole2 As Byte
            Guest
            Normal
            Admin
        End Enum

        Public Enum UserRole3 As Long
            Guest = Long.MaxValue
            Normal = 1
            Admin = 2
        End Enum

        Public Class HomeController
            <CheckPrivileges(New UserRole() {UserRole.Admin}, New UserRole2() {UserRole2.Admin}, New UserRole3() {UserRole3.Guest})>
            Public Async Function ActionOnlyAdminsCanDo() As Task
                Await Task.Delay(100)
            End Function
        End Class

        <Injection(GetType(CheckPrivilegesAspect))>
        <AttributeUsage(AttributeTargets.Method)>
        Public NotInheritable Class CheckPrivileges
            Inherits Attribute

            Public ReadOnly Property Roles As UserRole()

            Public Sub New(ByVal roles As UserRole(), ByVal roles2 As UserRole2(), ByVal role3s As UserRole3())
                Me.Roles = roles
            End Sub
        End Class

        <Aspect(Scope.PerInstance)>
        Public Class CheckPrivilegesAspect
            <Advice(Kind.Before)>
            Public Sub Before(
            <Argument(Source.Triggers)> ByVal attributes As Attribute())
                Dim cp As CheckPrivileges = Nothing
                If CSharpImpl.__Assign(cp, TryCast(attributes(0), CheckPrivileges)) IsNot Nothing AndAlso cp.Roles.Contains(UserRole.Admin) Then Passed = True
            End Sub

            Private Class CSharpImpl
                <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
                Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                    target = value
                    Return value
                End Function
            End Class
        End Class
    End Class
End Namespace
