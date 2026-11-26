<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="WebFormApp.People.Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .div-form{
            margin-left:30%;
            position:relative;
        }

        .text{
            width:800px;
        }
    </style>
    
    <div class="div-form">
        <div class="form-group">
            <asp:Label Text="CPF" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control" ID="txtCPF" />
        </div>
        <div class="form-group">
            <asp:Label Text="Name" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control text" ID="txtName" />
        </div>
        <div class="form-group">
            <asp:Label Text="Salary" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control" ID="txtSalary" />
        </div>
        <div class="form-group">
            <asp:Label Text="Birthday" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control" ID="txtBirthday" />
        </div>
        <div class="form-group">
            <asp:Label Text="Gender" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control" ID="txtGender" />
        </div>
        <div class="form-group">
            <asp:Label Text="Status" runat="server" />
            <asp:RadioButton Text="Active" runat="server" Checked="true" ID="rdActive" GroupName="status" />
            <asp:RadioButton Text="Inactive" runat="server" ID="rdInactive" GroupName="status" />
        </div>
        <div class="form-group">
            <asp:Label Text="Description" runat="server" />
            <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" MaxLength="200" TextMode="MultiLine" />
        </div>
        <div class="form-group">
            <hr />
        </div>
        <div class="form-group">
            <button type="button" class="btn btn-danger">Delete</button>
            <button type="button" class="btn btn-warning">Edit</button>
            <button type="button" class="btn btn-default">primary</button>
        </div>
        <div class="form-group">
            <button type="reset" class="btn btn-default">Clear</button>
            <button type="button" class="btn btn-success">Create</button>
        </div>
    </div>
    
</asp:Content>
