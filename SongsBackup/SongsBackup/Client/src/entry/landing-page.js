
    import { createApp, h, defineComponent } from "vue/dist/vue.esm-bundler.js";
    import wrapper from "vue3-webcomponent-wrapper";
    import Page from "../pages/landing-page.vue";
    
    const p = Page.props ? Object.keys(Page.props).map(_ => ':' + _ + '="' + _ + '"').join(' ') : null;
    
    const proxy = defineComponent({
        template: '<Suspense><template #fallback></template><Page ' + p + ' /></Suspense>',
        props: Page.props,
        components: {
            Page
        }
    });
    
    window.customElements.define('landing-page', wrapper(proxy, createApp, h));