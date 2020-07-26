<template>
  <div id="app" class="uk-container">
    <Counters />
  </div>
</template>

<script>
import UIkit from "uikit";
import Icons from "uikit/dist/js/uikit-icons";
import Counters from "@/components/Counters";

UIkit.use(Icons);

export default {
  name: "App",
  components: {
    Counters,
  },
};
</script>

<style lang="less">
@import "uikit/src/less/uikit.theme.less";

#app {
  margin-top: 200px;
}
</style>