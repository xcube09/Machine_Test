var SalesRecordsViewModel = function () {
    var self = this;

    self.pageSize = ko.observable(2);
    self.data = ko.observableArray([]);
    self.totalCount = ko.observable(0);

    self.searchQuery = {
        countryCode: ko.observable(),
        regionCode: ko.observable(),
        cityCode: ko.observable(),
        dateOfSale: ko.observable()
    };

    self.countriesList = ko.observableArray([]);
    self.regionsList = ko.observableArray([]);
    self.citiesList = ko.observableArray([]);

    self.searchQuery.countryCode.subscribe(function (countryCode) {
        self.searchQuery.regionCode(null); 
        self.regionsList([]);
        if (countryCode) {
            $.blockUI();
            $.ajax({
                url: `/api/countries/${countryCode}/regions`,
                type: 'get',
                success: response => {
                    self.regionsList(response.regions);
                },
                complete: _ => { $.unblockUI(); }
            });
        }
    });

    self.searchQuery.regionCode.subscribe(function (regionCode) {
        
        self.searchQuery.cityCode(null); 
        self.citiesList([]);

        if (regionCode) {
            $.blockUI();

            $.ajax({
                url: `/api/regions/${regionCode}/cities`,
                type: 'get',
                success: response => {
                    self.citiesList(response.cities);
                },
                error: xhr => { console.log(xhr); },
                complete: _ => { $.unblockUI(); }
            });
        }  
    });


    self.Paging = ko.observable(new PagingVm({
        pageSize: self.pageSize(),
        totalCount: self.totalCount(),
    }));


    self.currentPageSubscription = self.Paging().CurrentPage.subscribe(function (page) {

        fetchData(page);     
    });



    self.dispose = function () {
        self.currentPageSubscription.dispose();
    }

    function fetchData(page) {
        $.blockUI();

        $.ajax({
            url: `/api/sales/search?page=${page}&pageSize=${self.pageSize()}`,
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(self.searchQuery),
            success: response => {
                console.log(response);
                self.data(response.data);
                self.totalCount(response.totalCount);

                self.Paging().Update({
                    PageSize: self.Paging().PageSize(),
                    TotalCount: self.totalCount(),
                    CurrentPage: page
                });
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
     
    }


    self.search = function () {
        fetchData(1);
    }

    self.reset = function () {
        self.searchQuery.cityCode(null);
        self.searchQuery.regionCode(null);
        self.searchQuery.countryCode(null);
        self.searchQuery.dateOfSale(null);

        self.search();
    }


    function init() {
        fetchData(1);     

        $.ajax({
            url: '/api/countries',
            type: 'get',
            success: response => {
                self.countriesList(response.countries);
            }
        });
    }
    init();
}