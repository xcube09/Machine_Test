var AddSalesRecordViewModel = function () {
    var self = this;

    self.loading = ko.observable(false);

    self.command = {
        customerName: ko.observable().extend({ required: true }),
        cityCode: ko.observable().extend({ /*required: true, number: true*/ }),
        dateOfSale: ko.observable().extend({ required: true }),
        productID: ko.observable().extend({ required: true }),
        quantity: ko.observable().extend({ required: true, number: true }),
        selectedCountryCode: ko.observable().extend({ required: true }),
        selectedRegionCode: ko.observable().extend({ required: true })
    };


    self.productList = ko.observableArray([]);
    self.countriesList = ko.observableArray([]);
    self.regionsList = ko.observableArray([]);
    self.citiesList = ko.observableArray([]);


    self.validationResult = ko.validatedObservable(self.command);

    self.submit = function () {

        if (!self.validationResult.isValid()) {
            self.validationResult.errors.showAllMessages();
            return false;
        }

        console.log(ko.toJS(self.command));
        $.blockUI();

        $.ajax({
            url: '/api/sales',
            type: 'post',
            contentType: 'application/json',
            data: ko.toJSON(self.command),
            success: result => {
                if (result.success) {
                    Swal.fire(
                        'Success',
                        'operation was successful!',
                        'success'
                    ).then(function () {
                        window.location.href = '/SalesRecords';
                    });
                }
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
    }

    self.command.selectedCountryCode.subscribe(function (countryCode) {
        if (self.command.selectedRegionCode()) {
            self.command.selectedRegionCode('');
        }        

        $.blockUI();

        $.ajax({
            url: `/api/countries/${countryCode}/regions`,
            type: 'get',
            success: response => {
                self.regionsList(response.regions);
            },
            complete: _ => { $.unblockUI(); }
        });
    });

    self.command.selectedRegionCode.subscribe(function (regionCode) {
        if (self.command.cityCode()) {
            self.command.cityCode('');
        }
        
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
    });


      

    function init() {
        $.blockUI();

        $.ajax({
            url: '/api/sales/post-get',
            type: 'get',
            success: response => {
                console.log(response);
                self.productList(response.products);
                self.countriesList(response.countries);
            },
            error: xhr => { console.log(xhr); },
            complete: _ => { $.unblockUI(); }
        });
        
    }
    init();
}