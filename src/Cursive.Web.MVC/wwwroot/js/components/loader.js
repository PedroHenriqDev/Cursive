Vue.component('loader', {
    template: `
            <div v-if="isActive" class="loader-overlay">
                 <div class="spinner"></div>
            </div>
              `,
    data() {
        return {
            isActive: false
        };
    },
    methods: {
        showLoader() {
            this.isActive = true;
        },
        hideLoader() {
            this.isActive = false;
        },
        created() {
            this.$root.$on('show-loader', this.showLoader);
            this.$root.$on('hide-loader', this.hideLoader);
        }
    }
});
