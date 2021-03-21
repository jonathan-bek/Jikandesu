module JdRowItemDirective {
    export class JdRowItemDirective implements ng.IDirective {
        restrict = "E";
        //require = 'ngModel';
        template = "<p>TEST JD ROW ITEM DIRECTIVE</p>"
        replace = true;

        //constructor for dependencies/svcs

        link = (scope: ng.IScope, element: ng.IAugmentedJQuery,
            attrs: ng.IAttributes, ctrl: any) => {
        }

        static factory(): ng.IDirectiveFactory {
            const directive = () => new JdRowItemDirective();
            directive.$inject = [] as any[];
            return directive;
        }
    }
}
