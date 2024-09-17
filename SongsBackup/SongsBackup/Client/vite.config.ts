import vue from '@vitejs/plugin-vue';
import fs from "fs";
import glob from 'glob';
import path from "path";
import { defineConfig } from 'vite';
// import cssInjectedByJsPlugin from 'vite-plugin-css-injected-by-js';

const entryDir = './src/entry';

if (fs.existsSync(entryDir)) {
    fs.rmSync(entryDir, { recursive: true });
}
fs.mkdirSync(entryDir);

function getComponentName(filePath) {
    return path.basename(filePath, path.extname(filePath));
}

function writePageEntryFile(componentName, filePath) {
    const relativeImportPath = path.relative(entryDir, filePath).replace(/\\/g, '/');

    const s = `
    import { createApp, h, defineComponent } from "vue/dist/vue.esm-bundler.js";
    import wrapper from "vue3-webcomponent-wrapper";
    import Page from "${relativeImportPath}";
    
    const p = Page.props ? Object.keys(Page.props).map(_ => ':' + _ + '="' + _ + '"').join(' ') : null;
    
    const proxy = defineComponent({
        template: '<Suspense><template #fallback></template><Page ' + p + ' /></Suspense>',
        props: Page.props,
        components: {
            Page
        }
    });
    
    window.customElements.define('${componentName}', wrapper(proxy, createApp, h));`;

    fs.writeFileSync(`${entryDir}/${componentName}.js`, s);
}

glob.sync("./src/pages/**/*.vue", { absolute: true }).forEach(filePath => {
    const componentName = getComponentName(filePath);
    writePageEntryFile(componentName, filePath);
});

export default defineConfig({
    plugins: [vue()],
    build: {
        emptyOutDir: true,
        rollupOptions: {
            input: {
                ...fs.readdirSync(entryDir).reduce((entryPoints, filePath) => {
                    const componentName = getComponentName(filePath);
                    entryPoints[componentName] = `${entryDir}/${componentName}.js`;
                    return entryPoints;
                }, {}),
            },
            output: {
                dir: "../wwwroot/pages",
                entryFileNames: "[name].js",
                chunkFileNames: "[name].[hash].js",
                assetFileNames: "assets/[name].[ext]"
            }
        }
    },
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
            "@components": path.resolve(__dirname, "./src/components"),
            "@pages": path.resolve(__dirname, "./src/pages")
        },
    },
});