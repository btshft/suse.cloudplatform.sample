<template>
  <div class="uk-child-width-expand" uk-grid>
    <div class="uk-width-1-5"></div>
    <div class="uk-grid-collapse uk-child-width-expand" uk-grid>
      <div>
        <div class="uk-card uk-card-primary uk-card-body">
          <h3 class="uk-card-title">Local</h3>
          <div>
            <p>
              Local counter is:
              <span v-if="!this.busy"> {{ this.counters.local }} </span>
              <span
                v-if="this.busy"
                class="uk-margin-small-left"
                uk-spinner="ratio: 0.5"
              ></span>
            </p>
          </div>
        </div>
      </div>
      <div>
        <div class="uk-card uk-card-secondary uk-card-body">
          <h3 class="uk-card-title">Global</h3>
          <div>
            <p>
              Global counter is:
              <span v-if="!this.busy"> {{ this.counters.global }} </span>
              <span
                v-if="this.busy"
                class="uk-margin-small-left"
                uk-spinner="ratio: 0.5"
              ></span>
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="uk-width-1-5"></div>
  </div>
</template>

<script>
import api from "@/api/counters";

export default {
  data() {
    return {
      busy: true,
      counters: {
        local: 0,
        global: 0,
      },
    };
  },

  async mounted() {
    try {
      const local = await api.methods.incrementLocal();
      this.counters.local = local.value;

      const global = await api.methods.incrementGlobal();
      this.counters.global = global.value;
    } finally {
      this.busy = false;
    }
  },
};
</script>