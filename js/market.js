$(function () {
    var apps = [
        {
            title : 'test app',
            icon : 'http://files.softicons.com/download/application-icons/48px-icons-1-4-by-leonard-schwarz/png/48x48/Map.png',
            installed : true
        },
        {
            title : 'testing application',
            icon : 'http://cdn5.iconfinder.com/data/icons/48px_icons_collection_by_neweravin/48/adress_book.png',
            installed : false
        },
        {
            title : 'test app',
            icon : 'http://files.softicons.com/download/application-icons/48px-icons-1-4-by-leonard-schwarz/png/48x48/Map.png',
            installed : true
        },
        {
            title : 'testing application',
            icon : 'http://cdn5.iconfinder.com/data/icons/48px_icons_collection_by_neweravin/48/flash.png',
            installed : true
        }
    ];

    //yeah...
    $(document).ready(function() {
        //activate our market
        $('#marketModal').modal('show');
        //test out our template
        $('#appResultTemplate').tmpl(apps).appendTo('#appResultsBody');
    });
})