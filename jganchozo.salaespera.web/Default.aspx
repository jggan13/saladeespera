<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="jganchozo.salaespera.web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">


    <script src="https://code.jquery.com/jquery-3.4.1.js" integrity="sha256-WpOohJOqMqqyKL9FccASB9O0KwACQJpFTUBLTYOVvVU=" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <style>
        label.mostrar {
            border: 2px solid black; /* THIS SHOULD CHANGE */
            padding: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container m-5">
            <h1 class="text-center">Ticket de atencion al cliente</h1>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-4">
                    <label>Id</label>
                    <br />
                    <input type="text" id="id"/>
                </div>
                <div class="col-4">
                    <label>Nombre</label>
                    <br />
                    <input type="text" id="name" />
                </div>
                <div class="col-4">
                    <br />
                    <button id="btnGuardar" class="btn btn-outline-primary" type="button">Guardar</button>
                </div>
            </div>

        </div>

        <div class="container m-5">
            <div class="row">
                <div class="col-12">
                    <strong> COLA 1 </strong><div id="colaUno"></div>
                </div>
                <div class="col-12">
                    <strong>COLA 2 </strong><div id="colaDos"></div>
                </div>
            </div>
        </div>

    </form>

    <script>  
        $(document).ready(function () {

            console.log('iniciando...');
            $.ajax({
                    type: "POST",
                    url: "Default.aspx/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{}',
                    success: function (response) {
                        $('#name').val('');
                        $('#id').val('');
                        $('#colaUno').empty();
                        $('#colaDos').empty();
                        $.each(response.d, function (index, value) {
                            if (value.NumeroCola == 1) {
                                $('#colaUno').append('<label class="mostrar">' + value.NumeroAtencion + '</label> ');
                            } else {
                                $('#colaDos').append('<label class="mostrar">'+value.NumeroAtencion+'</label> ');
                            }
                            console.log(value.NumeroAtencion);
                        })
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });


            $('#btnGuardar').click(function () {
                var name = $('#name').val();
                var id = $('#id').val();
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/GuardarNuevoTurno",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{Id:"' + id + '", Nombre: "' + name + '"}',
                    success: function (response) {
                        console.log('funcionando...');
                        $('#name').val('');
                        $('#id').val('');
                        $('#colaUno').empty();
                        $('#colaDos').empty();
                        $.each(response.d, function (index, value) {
                            if (value.NumeroCola == 1) {
                                $('#colaUno').append('<label class="mostrar">' + value.NumeroAtencion + '</label> ');
                            } else {
                                $('#colaDos').append('<label class="mostrar">'+value.NumeroAtencion+'</label> ');
                            }
                            console.log(value.NumeroAtencion);
                        })
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });

        });
    </script>
</body>
</html>
