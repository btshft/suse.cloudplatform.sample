<template>
  <div
    id="app"
    class="uk-container"
  >
    <Counters />
    <div class="uk-text-center uk-margin-top uk-text-muted">
      <p>App IP: <code>{{ serverIp }}</code> </p>
    </div>
  </div>
</template>

<script>
import UIkit from "uikit";
import Icons from "uikit/dist/js/uikit-icons";
import Counters from "@/components/Counters";
import AppInfoApi from "@/api/appInfo.js";

UIkit.use(Icons);

export default {
  name: "App",
  components: {
    Counters,
  },

  data () {
    return {
      clientIp: null,
      serverIp: null
    }
  },

  async created () {
    const appInfo = await AppInfoApi.methods.get();
    if (appInfo.serverIp) {
      this.serverIp = appInfo.serverIp;
    }
  }
};
</script>

<style lang="less">
@import "uikit/src/less/uikit.theme.less";

#app {
  margin-top: 200px;
}
</style>