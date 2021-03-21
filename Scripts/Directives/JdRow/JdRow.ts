module JdRowDirective {
    export class JdRowDirective implements ng.IDirective {
        restrict = "AE";
        template = "<p>{{value}}</p>"
        scope = {
            value: "=value"
        };
        replace = true;

        //constructor for dependencies/svcs

        link = (scope: ng.IScope, element: ng.IAugmentedJQuery,
            attrs: ng.IAttributes, ctrl: any) => {
        }

        static factory(): ng.IDirectiveFactory {
            const directive = () => new JdRowDirective();
            directive.$inject = [] as any[];
            return directive;
        }
    }
}
