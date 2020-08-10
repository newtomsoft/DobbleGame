;(function($) {
    function PreLoad(imgs, options) {
        this.imgs = (typeof imgs === 'string') ? [imgs] : imgs;
        this.opts = $.extend({}, this.DEFAULTS, options);

        if(this.opts.ordered) {
            this._ordered();
        } else {
            this._unordered();
        }
    }

    PreLoad.DEFAULTS = {
        ordered: true,
        eachOnLoad: null,  
        allOnLoad: null  
    };

    PreLoad.prototype = {
        _unordered: function() {   
            var imgs = this.imgs,
                opts = this.opts,
                count = 0,
                len = imgs.length;

            $.each(imgs, function(i, src) {
                if(typeof src != 'string') return;

                var imgObj = new Image();

                $(imgObj).on("load error", function() {
                    opts.eachOnLoad && opts.eachOnLoad(++count);

                    if(count >= len) {
                        opts.allOnLoad && opts.allOnLoad();
                    }
                });

                imgObj.src = src;
            });
        },
        _ordered: function() {   
            var imgs = this.imgs,
                opts = this.opts,
                count = 0,
                len = imgs.length;

            load();
            function load() {
                var imgObj = new Image();
                $(imgObj).on("load error", function() {
                    count++;
                    if(count >= len) {
                        this.allOnLoad && this.allOnLoad();
                    } else {
                        this.eachOnLoad && this.eachOnLoad(count);
                        load();
                    }
                });

                imgObj.src = imgs[count];
            }
        }
    };

    $.extend({
        preload: function(imgs, options) {
            new PreLoad(imgs, options);
        }
    });
})(jQuery);